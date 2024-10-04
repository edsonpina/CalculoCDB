import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface AplicacaoResponse {
  cdi: string;
  imposto: string;
  tb: string;
  totalImposto: string;
  totalLiquido: string;
  vf: string;
  vi: string;
}
@Injectable({
  providedIn: 'root'
})
export class CalculocdbService {
  private apiUrl = 'http://localhost:44352';
  //private apiUrl = '/api'
  constructor(private http: HttpClient) { }

  getAplicacao(valorInicial: number, meses: number): Observable<AplicacaoResponse[]> {
    const headers = new HttpHeaders()
      .set('valorInicial', valorInicial.toString())
      .set('meses', meses.toString());

    return this.http.get<AplicacaoResponse[]>(`${this.apiUrl}/CalculoCDB`, { headers });
  }
}
