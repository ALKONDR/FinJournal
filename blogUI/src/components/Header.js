import React from 'react';
import NameAndLogo from './NameAndLogo';
import Search from './Search';

class Header extends React.Component {

  render() {
    return (
      <div className="header">
        <NameAndLogo />
        <Search />
      </div>
    );
  }
}

module.exports = Header;
