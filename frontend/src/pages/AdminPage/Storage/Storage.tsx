import Input from 'components/inputs/Input/Input.tsx';
import { useState } from 'react';
import videoCardImd from 'assets/videocard.jpg';
import styles from './Storage.module.css';
import StorageAdd from './StorageAdd/StorageAdd.tsx';
import useBorderValues from 'hooks/useBorderValues.ts';

function Storage() {
  const [productId, setProductId] = useState('');
  const [productCount, setProductCount] = useBorderValues(123, 0);
  return (
    <div className={styles.storage}>
      <Input
        type={'number'}
        value={productId}
        onChange={newProductId => setProductId(newProductId)}
        placeholder={'Введите id товара'}
      />
      <h3 className={styles.productName}>Gigabyte RTX 3080</h3>
      <img src={videoCardImd} alt="product" className={styles.productImg} />
      <h5 className={styles.productCount}>Всего: {productCount}</h5>
      <StorageAdd setCount={setProductCount} count={productCount} />
    </div>
  );
}

export default Storage;
