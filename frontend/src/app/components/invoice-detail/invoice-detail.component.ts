import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Invoice } from '../../models/invoice.model';
import { ActivatedRoute, RouterModule,  } from '@angular/router';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoice-detail',
  imports: [CommonModule, RouterModule],
  templateUrl: './invoice-detail.component.html',
  styleUrl: './invoice-detail.component.css'
})
export class InvoiceDetailComponent {
  invoice: Invoice | null = null;
  error: string | null = null;
  
  constructor(
    private route: ActivatedRoute,
    private invoiceService: InvoiceService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.error = 'Id de factura inválido';
      return;
    }

    this.invoiceService.getInvoiceById(id).subscribe({
      next: (invoice) => {
        this.invoice = invoice;
      },
      error: (err) => {
        this.error = 'Error al cargar la factura';
      }
    });
  }
}
