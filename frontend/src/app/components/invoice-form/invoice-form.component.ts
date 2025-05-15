import { CommonModule, NgForOf } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoice-form',
  imports: [ReactiveFormsModule, NgForOf, RouterModule, CommonModule],
  templateUrl: './invoice-form.component.html',
  styleUrl: './invoice-form.component.css'
})
export class InvoiceFormComponent {
  form: FormGroup;
  loading = false;
  invoiceId?: number;
  isEdit = false;
  originalDetailIds: number[] = [];

  constructor(
    private fb: FormBuilder, 
    private router: Router,
    private route: ActivatedRoute,
    private invoiceService: InvoiceService
  ) {
    this.form = this.fb.group({
      client: ['', Validators.required],
      date: [new Date().toISOString().substring(0, 10), Validators.required],
      details: this.fb.array([])
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.invoiceId = +idParam;
      this.isEdit = true;
      this.loadInvoice(this.invoiceId);
    } else {
      this.addDetail();
    }
  }

  loadInvoice(id: number) {
    this.loading = true;
    this.invoiceService.getInvoiceById(id).subscribe({
      next: (invoice) => {
        this.originalDetailIds = invoice.details.map(d => d.id);
        this.form.patchValue({
          client: invoice.client,
          date: invoice.date
        });
        this.details.clear();
        invoice.details.forEach(d => this.details.push(this.createDetail(d)));
        this.loading = false;
      },
      error: () => {
        alert('No se pudo cargar la factura');
        this.router.navigate(['/invoices']);
      }
    });
  }

  createDetail(detail?: any): FormGroup {
    return this.fb.group({
      id: [detail?.id || null],
      product: [detail?.product || '', Validators.required],
      quantity: [detail?.quantity || 1, [Validators.required, Validators.min(1)]],
      unitPrice: [detail?.unitPrice || 0, [Validators.required, Validators.min(0)]],
    });
  }

  addDetail() {
    this.details.push(this.createDetail());
  }

  removeDetail(index: number) {
    if (this.details.length > 1) {
      this.details.removeAt(index);
    }
  }

  submit() {
    if (this.form.valid) {
      const currentDetails = this.form.value.details;
      const currentIds = currentDetails.map((d: any) => d.id).filter((id: number | null) => id != null);      
      const deletedIds = this.originalDetailIds.filter(id => !currentIds.includes(id));

      const invoice = {
        ...this.form.value,
        total: this.total,
        deletedDetailIds: deletedIds
      };
      this.loading = true;
      console.log(invoice);
      this.invoiceService.createInvoice(invoice).subscribe({
        next: (res) => {
          this.router.navigate(['/invoices']);
        },
        error: (err) => {
          this.loading = false;
        }
      });
    }
  }

  get total(): number {
    return this.details.controls.reduce((sum, ctrl) => {
      const qty = ctrl.get('quantity')?.value || 0;
      const price = ctrl.get('unitPrice')?.value || 0;
      return sum + qty * price;
    }, 0);
  }

  get details(): FormArray {
    return this.form.get('details') as FormArray;
  }
}
