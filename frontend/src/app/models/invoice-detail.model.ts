export interface InvoiceDetail {
    id: number;
    invoiceId: number;
    product: string;
    quantity: number;
    unitPrice: number;
    subtotal: number;
}