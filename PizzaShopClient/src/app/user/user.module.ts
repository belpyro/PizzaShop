import { AuthGuard } from './../core/guards/pizzas.guard';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileComponent } from './components/profile/profile.component';

export const routes: Routes = [
  { path: '', component: ProfileComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [ProfileComponent],
  imports: [CommonModule, RouterModule.forChild(routes), ReactiveFormsModule],
  exports: [ProfileComponent],
})
export class UserModule {}
