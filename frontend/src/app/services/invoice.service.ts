import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Invoice } from '../models/invoice.model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private invoices: Invoice[] = [
    { id: 1, client: 'Carlos Pérez', date: '2025-05-10', total: 120000, details: []},
    { id: 2, client: 'Ana Gómez', date: '2025-05-12', total: 95000, details: [] },
    { id: 3, client: 'Luis Ramírez', date: '2025-05-13', total: 220000, details: [] }
  ];

  constructor() { }

  getInvoices(): Observable<Invoice[]> {
    return of(this.invoices);
  }

  createInvoice(invoice: Invoice): Observable<{ success: boolean; message: string }> {
    return of({success: true, message: "Factura creada"});
  }

  deleteInvoice(id: number): Observable<{ success: boolean; message: string }> {
    return of({success: true, message: "Factura eliminada"});
  }

  getInvoiceById(id: number): Observable<Invoice> {
    const mockInvoice: Invoice = {
      id,
      client: 'Juan Gonzalez',
      date: '2025-05-15',
      total: 150,
      details: [
        { id: 1, invoiceId: id, product: 'Product 1', quantity: 2, unitPrice: 25, subtotal: 50 },
        { id:2, invoiceId: id, product: 'Product 2', quantity: 5, unitPrice: 20, subtotal: 100 },
      ],
    };
    return of(mockInvoice);
  }
}
