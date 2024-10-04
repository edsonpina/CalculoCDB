import { Component } from '@angular/core';
import { CalculocdbService, AplicacaoResponse } from './services/calculocdb.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'calculo-cdb';
  aplicacaoResponse: AplicacaoResponse[] = [];
  valorInicial: number = 1000; 
  meses: number = 12; 
  errorMessage: string = '';

  constructor(private aplicacaoService: CalculocdbService) { }

  consultarAplicacao() {
    this.aplicacaoService.getAplicacao(this.valorInicial, this.meses).subscribe(
      (response: AplicacaoResponse[]) => {
        this.aplicacaoResponse = response;
        this.errorMessage = '';
      },
      error => {
        this.errorMessage = error.error; 
      }
    );
  }
}
