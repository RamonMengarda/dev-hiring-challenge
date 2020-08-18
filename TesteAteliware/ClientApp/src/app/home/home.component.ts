import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Topics } from '../../../../topics-enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public searchResults: SearchResults[];
  
  public checkedTopics = [];

  public baseUrl;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  //count how many checkboxes are selected
  private counter = 0;

  //create array with checkbox labels
  public topics = Topics;
  keys(): Array<string> {
    var keys = Object.keys(this.topics);
    return keys.slice(keys.length / 2);
  }

  //event on checkbox change
  checkboxChange(checkBox, topic, index) {
    if (checkBox.currentTarget.checked) {
      this.counter++;
      this.checkedTopics[index] = topic;
    }
    else {
      this.counter--;
      this.checkedTopics[index] = "";
    }
  }

  //check if checkbox should be disabled
  checkboxDisabled(checkBox) {
    if (checkBox.checked || (!checkBox.checked && this.counter < 5)) {
      return false;
    }
    else {
      return true;
    }
  }

  //check if at least one options is selected
  enableSearchButton() {
    if (this.counter > 0) {
      return true;
    }
    else {
      return false;
    }
  }

  //variable to disable the Search Button
  public disableSearchButon = false;

  //method triggered by the form submit,
  //save repos on database and return list
  onSubmit() {

    this.disableSearchButon = true;

    for (let topic in this.checkedTopics) {
      if (this.checkedTopics[topic] != "") {
        
        let params = new HttpParams().set("searchTerm", this.checkedTopics[topic]).set("allTopics", JSON.stringify(this.checkedTopics));

        var promise = this.http.get<SearchResults[]>(this.baseUrl + "searchResults", { params: params }).toPromise();
        console.log(promise);
        promise.then((data) => {
          this.searchResults = data;
        }).catch((error) => {
          console.log("Promise Failed " + (error));
        });
      }
    }
  }

  //on clicking save, replace save button with check image 
  public hideFavButton = false;

  //update the selected repo to favorite
  updateFavorite(id) {
    let params = new HttpParams().set("repoId", id.toString());

    var promise = this.http.get(this.baseUrl + "updateFavorite", { params: params }).toPromise();
    console.log(promise);
    promise.then((data) => {
      this.hideFavButton = true;
      alert("Repositório " + id + " adicionado aos favoritos.")
    }).catch((error) => {
      console.log("Promise Failed " + (error));
    });
  }
}

interface SearchResults {
  id: number;
  name: string;
  description: string;
  cloneUrl: string;
  createdAt: string;
  updatedAt: string;
  svnUrl: string;
  watchersCount: number;
  homepage: string;
}
