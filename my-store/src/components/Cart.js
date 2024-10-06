import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { removeProductFromCart } from '../actions/cartActions'; 
import { useNavigate } from 'react-router-dom';
import '../styles/Cart.css';

const Cart = () => {
  const { cartItems } = useSelector((state) => state.cart);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const totalPrice = cartItems.reduce((acc, item) => acc + (item.price * (item.quantity || 1)), 0);

  const handleCheckout = () => {
    if (cartItems.length > 0) {
      navigate('/checkout');
    } else {
      alert('Your cart is empty!');
    }
  };

  return (
    <div className="cart">
      <h1>Your Cart</h1>
      <ul>
        {cartItems.map((item) => (
          <li key={item.id}>
            <h2>{item.name}</h2>
            <p>Price: ${item.price.toFixed(2)}</p>
            <p>Quantity: {item.quantity || 1}</p>
            <button onClick={() => dispatch(removeProductFromCart(item.id))}>Remove</button>
          </li>
        ))}
      </ul>
      <h3>Total: ${totalPrice.toFixed(2)}</h3>
      <button className="checkout-button" onClick={handleCheckout}>Checkout</button>
    </div>
  );
};

export default Cart;
