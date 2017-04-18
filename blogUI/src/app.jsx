import React from 'react';
import ReactDom from 'react-dom';

class App extends React.Component {
  render() {
    return (
      <h1>{this.props.message}</h1>
    );
  }
}
App.propTypes = {
  message: React.PropTypes.string.isRequired,
};

ReactDom.render(<App message="" />, document.getElementById('root'));
