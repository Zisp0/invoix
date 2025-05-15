import { Routes } from '@angular/router';
import { InvoiceListComponent } from './components/invoice-list/invoice-list.component';
import { InvoiceFormComponent } from './components/invoice-form/invoice-form.component';

export const routes: Routes = [
    { path: '', redirectTo: 'invoices', pathMatch: 'full' },
    { path: 'invoices', component: InvoiceListComponent },
    { path: 'invoices/create', component: InvoiceFormComponent}
];
