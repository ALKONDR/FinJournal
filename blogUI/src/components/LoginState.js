import { observable } from 'mobx';

class LoginState {
  @observable displayLoginLayout = false;
}

module.exports = new LoginState();
