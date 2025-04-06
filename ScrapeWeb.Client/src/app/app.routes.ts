import { Routes } from '@angular/router';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'scrape',
    pathMatch: 'full'
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then((x) => x.LoginComponent),
  },
  {
    path: 'scrape',
    loadComponent: () => import('./pages/scrape/scrape.component').then((x) => x.ScrapeComponent),
    canActivate: [authGuard]
  },
  {
    path: 'report',
    loadComponent: () => import('./pages/report/report.component').then((x) => x.ReportComponent),
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: 'scrape'
  }
];
