import { ApiService } from './../../Services/api.service';
import { Idioma } from './../../Model/Idioma';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-add-knowledge',
  templateUrl: './add-knowledge.component.html',
  styleUrls: ['./add-knowledge.component.css']
})
export class AddKnowledgeComponent implements OnInit {

  txt: string;
  idIdioma: number;
  public supportedLanguages: Array<Idioma>;
  constructor(private api: ApiService) { }

  ngOnInit() {
    this.supportedLanguages = [];
    this.loadIdiomas();
    this.idIdioma = 0;
  }

  loadIdiomas() {
    this.api.getIdiomas().subscribe(response => {
      this.supportedLanguages = response.json();
      if (this.supportedLanguages.length > 0) {
        this.idIdioma = this.supportedLanguages[0].id;
      }
    }, error => {
      alert('No Languages were Found');
    });
  }

  tryAddKnowledge() {
    const id = Number(this.idIdioma);
    if (id > 0
      && this.txt != null
      && this.txt.length > 0) {
      this.api.setKnowledge(id, this.txt).subscribe(response => {
        alert('Knowledge Added Successfully');
      }, error => {
        alert('Adding Knowledge failed');
      });
    } else {
      alert('No enough Data for Knowledge adding');
    }
  }
}
