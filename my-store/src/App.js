import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProductCatalog from './components/ProductCatalog';
import Cart from './components/Cart';
import Register from './components/Register';
import Login from './components/Login';
import Checkout from './components/Checkout';
import Navbar from './components/Navbar';
import ProtectedRoute from './components/ProtectedRoute';

const App = () => {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/" element={<ProtectedRoute component={ProductCatalog} />} />
        <Route path="/cart" element={<ProtectedRoute component={Cart} />} />
        <Route path="/checkout" element={<ProtectedRoute component={Checkout} />} />
      </Routes>
    </Router>
  );
};

export default App;
