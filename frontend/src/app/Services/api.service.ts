import { Idioma } from './../Model/Idioma';
import { Http, RequestOptions, Headers, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ApiService {
  private baseURL = '';

  constructor(private http: Http) { }

  getIdiomas() {
    return this.http.get(this.baseURL + '/api/Classifier/Idioma');
  }
  postIdioma(data: Idioma) {
    const requestOptions = new RequestOptions();
    requestOptions.headers = new Headers();
    requestOptions.headers.append('Content-Type', 'application/json');
    requestOptions.headers.append('Accept', 'application/json');
    return this.http.post(this.baseURL + '/api/Classifier/Idioma', JSON.stringify(data), requestOptions);
  }
  setKnowledge(idIdioma: number, txt: string) {
    return this.http.post(this.baseURL + '/api/Classifier/setKnowledge/' + idIdioma.toString() + '?txt=' + txt, null);
  }

  classify(txt: string) {
    return this.http.post(this.baseURL + '/api/Classifier/classify/' + '?txt=' + txt, null);
  }

}


