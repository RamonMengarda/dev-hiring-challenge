import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public savedResults: SavedResults[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<SavedResults[]>(baseUrl + 'fetchData').subscribe(result => {
      this.savedResults = result;
    }, error => console.error(error));
  }
}

interface SavedResults {
  id: number;
  name: string;
  description: string;
  cloneUrl: string;
  svnUrl: string;
  watchersCount: number;
  homepage: string;
}
