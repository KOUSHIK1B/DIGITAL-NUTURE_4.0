import React from 'react';

class Cart extends React.Component {
  render() {
    return (
      <table border="1">
        <thead>
          <tr>
            <th>Item Name</th>
            <th>Price (INR)</th>
          </tr>
        </thead>
        <tbody>
          {this.props.items.map((item, index) => (
            <tr key={index}>
              <td>{item.itemname}</td>
              <td>{item.price}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }
}

export default Cart;
