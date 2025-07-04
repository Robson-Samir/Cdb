import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CDBService } from 'src/service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  constructor(private apiService: CDBService, private formbuilder: FormBuilder) {}
  title = 'Teste CDB';
  grossYield!: number; // Rendimentos Brutos
  netValue!: number; // Rendimentos Líquidos
  meuCalculo!: FormGroup;

  ngOnInit(): void {
    this.grossYield = 0;
    this.netValue = 0;
    this.meuCalculo = this.formbuilder.group({
      valorAplicado: [''],
      prazo: ['']
    });
  }

  onSubmit() {

    if (this.meuCalculo.invalid) {
      alert('Formulário inválido');
      return;
    }

    this.apiService.obterCalculo(this.meuCalculo.value.valorAplicado, this.meuCalculo.value.prazo).subscribe({
          next: (data : any) => {
            this.grossYield = data.grossYield; // Rendimentos Brutos
            this.netValue = data.netValue; // Rendimentos Líquidos
          },
          error: (error: HttpErrorResponse) => {
            alert('Erro ao obter dados:' + error.error);
          }
        });
   }

   limparFormulario() {
    this.grossYield = 0;
    this.netValue = 0;
    this.meuCalculo.reset();
  }
}
