export class User {
  id_user: number;
  userName: string | null | undefined;
  lastName?: string | null | undefined;
  password?: string | null | undefined;
  roleId?: number;

   constructor() {    
    this.id_user = 1;            
    this.userName = '' ;
    this.lastName = '';
    this.password = '';
    this.roleId = 1;

  }
}
