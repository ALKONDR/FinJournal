import { observable } from 'mobx';
import api from '../utils/api';

class LoginState {
  @observable displayLoginLayout = false;
  @observable username = '';
  @observable password = '';
  @observable userLoggedIn = api.userLoggedIn;
}

module.exports = new LoginState();
