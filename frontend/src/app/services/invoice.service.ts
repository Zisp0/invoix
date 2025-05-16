import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Invoice } from '../models/invoice.model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private http = inject(HttpClient);
  private apiUrl = 'https://invoix-n9z6.onrender.com/api/invoices';

  constructor() { }

  getInvoices(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.apiUrl);
  }

  createInvoice(invoice: Invoice): Observable<any> {
    return this.http.post(this.apiUrl, invoice);
  }

  updateInvoice(invoice: Invoice): Observable<any> {
    return this.http.put(this.apiUrl, invoice);
  }

  deleteInvoice(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getInvoiceById(id: number): Observable<Invoice> {
    return this.http.get<Invoice>(`${this.apiUrl}/${id}`);
  }
}
