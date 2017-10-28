import { ApiService } from './Services/api.service';
import { AuthGuard } from './Auth/auth-guard.service';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeViewComponent } from './/Views/home-view/home-view.component';
import { TextAreaComponent } from './/Components/text-area/text-area.component';
import { PreditionDisplayComponent } from './/Components/predition-display/predition-display.component';
import { KnowledgeViewComponent } from './/Views/knowledge-view/knowledge-view.component';
import { AddKnowledgeComponent } from './/Components/add-knowledge/add-knowledge.component';
import { AddCategoryComponent } from './/Components/add-category/add-category.component';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

const views: Routes = [
  {
    path: '',
    component: HomeViewComponent
  },
  {
    path: 'knowledge',
    component: KnowledgeViewComponent
  },
  {
    path: 'not-authorized',
    component: NotAuthorizedComponent
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }

];

@NgModule({
  declarations: [
    AppComponent,
    NotAuthorizedComponent,
    PageNotFoundComponent,
    HomeViewComponent,
    TextAreaComponent,
    PreditionDisplayComponent,
    KnowledgeViewComponent,
    AddKnowledgeComponent,
    AddCategoryComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(views),
    HttpModule,
    FormsModule
  ],
  providers: [
    AuthGuard,
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
