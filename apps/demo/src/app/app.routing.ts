import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MazeComponent } from './components/maze/maze.component';

const routes: Routes = [
  { path: 'maze', component: MazeComponent },
  { path: '', redirectTo: '/maze', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
