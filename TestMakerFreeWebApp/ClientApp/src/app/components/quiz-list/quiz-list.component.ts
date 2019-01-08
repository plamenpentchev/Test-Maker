import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, Route } from '@angular/router';

@Component({
  selector: 'quiz-list',
  templateUrl: './quiz-list.component.html',
  styleUrls: ['./quiz-list.component.less'] // loading less styles using less-loader(npm). configured in webpack config
})
export class QuizListComponent implements OnInit {
  @Input() class: string;
  title: string;
  selectedQuiz: IQuiz;
  quizzies: IQuiz[];

  constructor(
      private http: HttpClient,
      @Inject('BASE_URL') private baseUrl: string,
      private router: Router
  )
  {
    this.title = 'Latest quizzies';
  }

  onSelect(quiz: IQuiz) {
    var v = quiz;
    this.selectedQuiz = v;
    this.router.navigate(["quiz/", v.QuizId]);
  }

  ngOnInit() {
    var url = this.baseUrl + 'api/quiz/';
    switch (this.class) {
      case "latest":
      default:
        this.title = "Latest quizzies";
        url += "latest/";
        break;
      case "byTitle":
        this.title = "Quizzues by title";
        url += "byTitle/";
        break;
      case "random":
        this.title = "Random quizzies";
        url += "random/";
        break;
    }
    this.http.get<IQuiz[]>(url).subscribe
      (
      result => { this.quizzies = result },
      error => console.log(error)
      );
  }

}
