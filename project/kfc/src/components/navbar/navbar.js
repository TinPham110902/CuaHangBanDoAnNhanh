/* eslint-disable jsx-a11y/anchor-is-valid */
<<<<<<< HEAD
import React from "react";
import "../../views/App.css";
=======
import React from 'react';
import "../../views/App.css"
import { Link } from 'react-router-dom';
>>>>>>> 8f6312b93595c38f238b4d116544e7d8531081b3
class Navbar extends React.Component {
  render() {
    return (
      <div className="nav-bar">
        <nav class="nav justify-content-center">
          <a class="nav-link active" href="/">
            Home
          </a>
          <a class="nav-link" href="/ThemNv">
            ThemNV
          </a>
          <a class="nav-link disabled" href="#">
            Disabled link
          </a>
        </nav>
      </div>
    );
  }
}

export default Navbar;
