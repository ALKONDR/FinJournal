/* eslint-disable no-console */

import React from 'react';
import { observer } from 'mobx-react';
import { Redirect } from 'react-router-dom';
import LoginState from './LoginState';
import api from '../utils/api';

@observer
class WriteArticle extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      title: '',
      description: '',
      content: '',
      redirect: false,
    };

    this.handleTitleChange = this.handleTitleChange.bind(this);
    this.handleDescriptionChange = this.handleDescriptionChange.bind(this);
    this.handleContentChange = this.handleContentChange.bind(this);
    this.publish = this.publish.bind(this);
    this.setState = this.setState.bind(this);
  }

  handleTitleChange(event) {
    this.setState({
      title: event.target.value,
    });
  }

  handleDescriptionChange(event) {
    this.setState({
      description: event.target.value,
    });
  }

  handleContentChange(event) {
    this.setState({
      content: event.target.value,
    });
  }

  publish(e) {
    e.preventDefault();

    const story = {
      title: this.state.title.trim(),
      description: this.state.description.trim(),
      content: this.state.content.trim(),
      author: api.loggedInUser,
    };

    const isValidArticle = story.title.length > 0 && story.content.length > 0;

    if (isValidArticle) {
      this.setState({
        title: '',
        description: '',
        content: '',
      });

      api.addArticle(api.loggedInUser, story)
        .then((response) => {
          if (response.status >= 200 && response.status < 300) {
            this.setState({ redirect: true });
          }
        })
        .catch(() => {
          api.refreshToken()
            .then((refreshed) => {
              if (refreshed) {
                api.addArticle(api.loggedInUser, story)
                  .then((response) => {
                    if (response.status >= 200 && response.status < 300) {
                      this.setState({ redirect: true });
                    }
                  })
                  .catch((error) => { console.log(error); });
              }
            })
            .catch((error) => { console.log(error); });
        })
        .catch((error) => { console.log(error); });
    }
  }

  render() {
    return (
      <div className="writeArticleContainer">
        {this.state.redirect || !LoginState.userLoggedIn ?
          <Redirect to="/" /> : null}
        <h2>
          Your cool title
        </h2>
        <input
          type="text"
          placeholder="Title"
          className="titleInput"
          onChange={this.handleTitleChange}
        />
        <h2>
          Description of your article
        </h2>
        <textarea
          type="text"
          placeholder="Description"
          className="descriptionArea"
          onChange={this.handleDescriptionChange}
        />
        <h2>
          Your article
        </h2>
        <textarea
          type="text"
          placeholder="Article"
          className="articleArea"
          onChange={this.handleContentChange}
        />
        <button className="button publishButton" onClick={this.publish}>
          Publish
        </button>
      </div>
    );
  }
}

module.exports = WriteArticle;
