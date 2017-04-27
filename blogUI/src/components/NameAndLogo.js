import React from 'react';
import { Link } from 'react-router-dom';

class NameAndLogo extends React.Component {
  render() {
    return (
      <Link to="/" >
        <div className="nameAndLogo" >
          <img alt="logo" className="logo" />
        </div>
      </Link>
    );
  }
}

module.exports = NameAndLogo;
