import { Component } from '@angular/core';
import { InvoiceService } from '../../services/invoice.service';
import { Invoice } from '../../models/invoice.model';
import { CommonModule, NgForOf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-invoice-list',
  imports: [NgForOf, RouterModule, CommonModule],
  templateUrl: './invoice-list.component.html',
  styleUrl: './invoice-list.component.css'
})
export class InvoiceListComponent {
  invoices: Invoice[] = [];

  constructor(
    private invoiceService: InvoiceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.invoiceService.getInvoices().subscribe(
      (data) => {
        this.invoices = data;
      },
      (error) => {
        console.error('Error al obtener las facturas', error);
      }
    );
  }

  
  deleteInvoice(id: number) {
    Swal.fire({
    title: '¿Estás seguro?',
    text: `Esto eliminará la factura #${id}.`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#3085d6',
    confirmButtonText: 'Sí, eliminar',
    cancelButtonText: 'Cancelar'
  }).then((result) => {
    if (result.isConfirmed) {
      this.invoiceService.deleteInvoice(id).subscribe(() => {
        this.invoices = this.invoices.filter(inv => inv.id !== id);
        Swal.fire('Eliminado', 'La factura fue eliminada correctamente.', 'success');
      });
    }
  });
  }
}
