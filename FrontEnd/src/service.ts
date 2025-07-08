import { HttpClient } from '@angular/common/http';
import { CalculoCDB } from './Model/CalculoCDB';
import { Injectable } from '@angular/core';

@Injectable({
providedIn: 'root'
})
export class CDBService {

  //private readonly API = 'https://localhost:7197/api/CDB';
  private readonly API = 'http://localhost:8080/api/CDB';

    constructor(private http: HttpClient) {}

    obterCalculo(valorAplicado: number, prazo: number) {
        return this.http.get<CalculoCDB[]>(`${this.API}?value=${valorAplicado}&months=${prazo}`);
    }
}
