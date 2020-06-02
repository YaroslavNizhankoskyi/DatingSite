import { MessagesComponent } from './messages/messages.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { HomeComponent } from './home/home.component';
import {Routes} from '@angular/router';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MemberDetailsResolver } from './_resolver/member-details.resolver';
import { MemberListResolver } from './_resolver/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolver/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListsResolver } from './_resolver/lists.resolver';
import { MessagesResolver } from './_resolver/messages.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
        { path: 'members', component: MemberListComponent, canActivate: [AuthGuard],
        resolve: { users: MemberListResolver}},
        { path: 'members/:id', component: MemberDetailsComponent, canActivate: [AuthGuard],
        resolve : { user : MemberDetailsResolver}},
        { path: 'member/edit', component: MemberEditComponent,
        resolve: { user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
        { path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
        { path: 'lists', component: ListsComponent,  resolve: {users: ListsResolver}},
      ]
    },
    { path: '**', redirectTo: '',
     pathMatch: 'full'}
];