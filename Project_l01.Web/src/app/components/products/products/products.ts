import { Component, OnInit, inject, signal } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product.module';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [RouterLink],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products implements OnInit {
  private readonly productService = inject(ProductService);

  products = signal<Product[]>([]);
  isLoading = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  deletingProductId = signal<number | null>(null);

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products.set(products);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading products:', error);

        this.errorMessage.set('Unable to load products. Please try again later.');

        this.isLoading.set(false);
      },
    });
  }

  deleteProduct(id: number): void {
    const confirmed = confirm('Are you sure you want to delete this product?');

    if (!confirmed) {
      return;
    }

    this.deletingProductId.set(id);
    this.errorMessage.set(null);

    this.productService.deleteProduct(id).subscribe({
      next: () => {
        this.products.update((products) => products.filter((product) => product.id !== id));

        this.deletingProductId.set(null);
      },
      error: (error) => {
        console.error('Error deleting product:', error);

        this.errorMessage.set('Unable to delete product. Please try again later.');

        this.deletingProductId.set(null);
      },
    });
  }
}
