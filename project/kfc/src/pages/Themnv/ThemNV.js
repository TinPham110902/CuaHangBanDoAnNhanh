import React from 'react';
import Navbar from '../../components/Navbar/Navbar';
function ThemNV(props) {
    return (
        <div>
            <Navbar />
            <div className='form'>
            <form className='input-nv'>
  <div class="form-group">
    <label for="email">Email address:</label>
    <input type="email" class="form-control" placeholder="Enter email" id="email"/>
  </div>
  <div class="form-group">
    <label for="pwd">Password:</label>
    <input type="password" class="form-control" placeholder="Enter password" id="pwd"/>
  </div>
  <div class="form-group form-check">
    <label class="form-check-label">
      <input class="form-check-input" type="checkbox"/> Remember me
    </label>
  </div>
  <button type="submit" class="btn btn-primary">Submit</button>
</form>
</div>
        </div>
    );
}

export default ThemNV;