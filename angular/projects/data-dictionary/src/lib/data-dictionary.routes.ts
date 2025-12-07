import { RouterOutletComponent } from '@abp/ng.core';
import { Routes } from '@angular/router';
import { DataDictionaryComponent } from './components/data-dictionary.component';

export const dataDictionaryRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: RouterOutletComponent,
    children: [
      {
        path: '',
        component: DataDictionaryComponent,
      },
    ],
  },
];
