import React from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logoutUser } from '../actions/authActions';
import '../styles/Navbar.css';

const Navbar = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const isAuthenticated = useSelector((state) => state.auth.isAuthenticated);

  const handleLogout = () => {
    dispatch(logoutUser());
    navigate('/login');
  };

  return (
    <nav className="navbar-container">
      <ul className="navbar">
        {isAuthenticated && (
          <>
            <li>
              <Link to="/" className={location.pathname === '/' ? 'active' : ''}>Products</Link>
            </li>
            <li>
              <Link to="/cart" className={location.pathname === '/cart' ? 'active' : ''}>Cart</Link>
            </li>
            <li className="user-section">
              <button onClick={handleLogout} className="logout-button">Logout</button>
            </li>
          </>
        )}
      </ul>
    </nav>
  );
};

export default Navbar;
