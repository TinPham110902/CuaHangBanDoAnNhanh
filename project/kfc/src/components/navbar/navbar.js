/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import "../../views/App.css"
import { Link } from 'react-router-dom';
class Navbar extends React.Component {
    render() {
        return (
            <div className='nav-bar'>
                <nav class="nav justify-content-center">
                    <a class="nav-link active" href="/">Home</a>
                    <a class="nav-link" href="/ThemNv">ThemNV</a>
                    <a class="nav-link disabled" href="#">Disabled link</a>
                </nav>
            </div>
        )
    }
}

export default Navbar;