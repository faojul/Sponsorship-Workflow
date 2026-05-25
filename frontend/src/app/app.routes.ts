import { Routes } from '@angular/router';
import { authChildGuard, authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./core/layouts/main-layout/main-layout')
        .then(m => m.MainLayout),
    canActivate: [authGuard],
    canActivateChild: [authChildGuard], 
     children: [
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard')
            .then(m => m.Dashboard)
      },
      {
        path: 'requests',
        loadComponent: () =>
          import('./features/sponsorship-requests/pages/request-list/request-list')
            .then(m => m.RequestList)
      },
      {
        path: 'requests/create',
        loadComponent: () =>
          import('./features/sponsorship-requests/pages/create-request/create-request')
            .then(m => m.CreateRequest)
      },
      {
        path: 'manager-approvals',
        loadComponent: () =>
          import('./features/manager-approvals/pages/manager-approval-list/manager-approval-list')
            .then(m => m.ManagerApprovalList)
      },
      {
        path: 'finance-approvals',
        loadComponent: () =>
          import('./features/finance-approvals/pages/finance-approval-list/finance-approval-list')
            .then(m => m.FinanceApprovalList)
      },
      {
        path: 'admin/requests',
        loadComponent: () =>
          import('./features/admin/pages/all-requests/all-requests')
            .then(m => m.AllRequests)
      },
      {
        path: 'workflow-history/:id',
        loadComponent: () =>
          import('./features/workflow-history/pages/workflow-history/workflow-history')
            .then(m => m.WorkflowHistory)
      },
      {
        path: 'sponsorship-types',
        loadComponent: () =>
          import('./features/sponsorship-types/pages/sponsorship-type-list/sponsorship-type-list')
            .then(m => m.SponsorshipTypeList)
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }
    ]
  },
  {
  path: 'login',
  loadComponent: () =>
    import('./features/auth/login/login')
      .then(m => m.Login)
},

];