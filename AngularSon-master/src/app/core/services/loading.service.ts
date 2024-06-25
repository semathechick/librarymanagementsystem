import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  isLoading: boolean = true;
  constructor() {}

  setLoading(value: boolean) {
    this.isLoading = value;
  }
}
