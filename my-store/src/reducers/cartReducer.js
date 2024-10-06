import { createSlice } from '@reduxjs/toolkit';

const cartSlice = createSlice({
  name: 'cart',
  initialState: {
    cartItems: [],
  },
  reducers: {
    addToCart: (state, action) => {
      const product = action.payload;
      const existItem = state.cartItems.find((item) => item.id === product.id);

      if (existItem) {
        // Увеличиваем количество, если продукт уже в корзине
        existItem.quantity += 1;
      } else {
        // Если товара нет в корзине, добавляем его с количеством 1
        state.cartItems.push({ ...product, quantity: 1 });
      }
    },
    removeFromCart: (state, action) => {
      state.cartItems = state.cartItems.filter((item) => item.id !== action.payload);
    },
    clearCart: (state) => {
      state.cartItems = []; 
    }
  },
});

export const { addToCart, removeFromCart, clearCart } = cartSlice.actions;
export default cartSlice.reducer;
