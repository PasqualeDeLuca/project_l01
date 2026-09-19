import { Component, OnInit, inject, signal } from '@angular/core';
import { ProductService } from '../../services/products/product.service';
import { Product } from '../../models/product.module';

@Component({
  selector: 'app-products',
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products implements OnInit {
  private readonly productService = inject(ProductService);

  products = signal<Product[]>([]);

  ngOnInit(): void {
    this.productService.getProducts().subscribe({
      next: (products) => {
        console.log('Prodotti ricevuti:', products);

        this.products.set(products);

        console.log('this.products:', this.products());
        console.log('this.products.length:', this.products().length);
      },
      error: (error) => {
        console.error('Error loading products:', error);
      },
    });
  }
}
