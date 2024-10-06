import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import axios from 'axios';
import { clearCart } from '../reducers/cartReducer';
import { useNavigate } from 'react-router-dom';
import '../styles/Checkout.css';

const Checkout = () => {
  const { cartItems } = useSelector((state) => state.cart);
  const { token, userId  } = useSelector((state) => state.auth);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const handleCheckout = async () => {
    if (cartItems.length === 0) {
      alert('Your cart is empty!');
      return;
    }

    try {
      const orderData = {
        userId: userId || 'Anonymous', 
        items: cartItems.map(item => ({
          productId: item.id,
          quantity: item.quantity || 1,
          unitPrice: item.price,
        })),
      };
      
      const response = await axios.post('http://localhost:5000/orders', orderData, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      alert('Order placed successfully!');

      dispatch(clearCart());

      navigate('/');
    } catch (error) {
      console.error('Error placing order:', error);
      alert('Failed to place order.');
    }
  };

  return (
    <div className="checkout">
      <h1>Checkout</h1>
      <button className="checkout-button" onClick={handleCheckout}>Place Order</button>
    </div>
  );
};

export default Checkout;
