import { PreditionDisplayComponent } from './../predition-display/predition-display.component';
import { ApiService } from './../../Services/api.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-text-area',
  templateUrl: './text-area.component.html',
  styleUrls: ['./text-area.component.css']
})
export class TextAreaComponent implements OnInit {

  @Input('p') p: PreditionDisplayComponent;
  txt: string;
  constructor(private api: ApiService) { }

  ngOnInit() {
  }
  tryClassify() {
    if (this.txt != null
      && this.txt.length > 0) {
      this.api.classify(this.txt).subscribe(response => {
        this.p.setTendencies(response.json());
      }, error => {
        alert('Server failed');
      });
    } else {
      alert('Provide Text for Classification');
    }
  }

  onModelChange() {
    this.p.onTextEdit();
  }
}
