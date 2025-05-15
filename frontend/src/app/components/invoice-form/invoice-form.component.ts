import { CommonModule, NgForOf } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
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

  constructor(
    private fb: FormBuilder, 
    private router: Router,
    private invoiceService: InvoiceService
  ) {
    this.form = this.fb.group({
      client: ['', Validators.required],
      date: [new Date().toISOString().substring(0, 10), Validators.required],
      details: this.fb.array([
        this.createDetail()
      ])
    });
  }

  createDetail(): FormGroup {
    return this.fb.group({
      product: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [0, [Validators.required, Validators.min(0)]]
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
      const invoice = {
        ...this.form.value,
        total: this.total
      };
      this.loading = true;
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
