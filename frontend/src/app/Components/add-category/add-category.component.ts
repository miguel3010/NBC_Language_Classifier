import { ApiService } from './../../Services/api.service';
import { Idioma } from './../../Model/Idioma';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {

  idioma: Idioma;
  @Input('listIdiomas') listIdiomas: Idioma[];

  constructor(private api: ApiService) {
    this.idioma = new Idioma();
  }

  ngOnInit() {
  }

  tryAddNewIdioma() {
    this.idioma.id = 0;
    if (this.idioma.nombre != null
      && this.idioma.nombre.length > 0) {
      this.api.postIdioma(this.idioma).subscribe(response => {
        this.listIdiomas.push(response.json());
        this.idioma = new Idioma();
        alert('Added new Language');
      }, error => {
        alert('Add new Language Failed');
      });
    } else {
      alert('Give a name for the language');

    }
  }

}
