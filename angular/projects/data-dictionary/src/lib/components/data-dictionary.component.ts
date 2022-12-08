import { Component, OnInit } from '@angular/core';
import { DataDictionaryService } from '../services/data-dictionary.service';

@Component({
  selector: 'lib-data-dictionary',
  template: ` <p>data-dictionary works!</p> `,
  styles: [],
})
export class DataDictionaryComponent implements OnInit {
  constructor(private service: DataDictionaryService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
