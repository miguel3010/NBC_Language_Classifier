import { Tendencia } from './../../Model/Tendencia';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-predition-display',
  templateUrl: './predition-display.component.html',
  styleUrls: ['./predition-display.component.css']
})
export class PreditionDisplayComponent implements OnInit {

  tendencias: Tendencia[];
  first: string;


  constructor() {
    this.first = 'Provide some text for clasification';
  }

  ngOnInit() {
  }

  setTendencies(tendencias: Tendencia[]) {
    if (tendencias != null && tendencias.length > 0) {
      this.tendencias = tendencias;
      this.first = this.tendencias[0].idioma.nombre;
    } else {
      this.tendencias = [];
      this.first = 'No Classifications were found';
    }
  }

  onTextEdit() {
    this.tendencias = [];
    this.first = 'Press for classify buttom for process text';
  }

}

