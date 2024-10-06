import { addToCart, removeFromCart } from '../reducers/cartReducer';

export const addProductToCart = (product) => (dispatch) => {
  dispatch(addToCart(product));
};

export const removeProductFromCart = (productId) => (dispatch) => {
  dispatch(removeFromCart(productId));
};
