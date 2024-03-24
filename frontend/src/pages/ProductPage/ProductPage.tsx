import { useNavigate, useParams } from 'react-router-dom';
import Container from 'components/Container/Container.tsx';
import styles from './ProductPage.module.css';
import ProductMain from './ProductMain/ProductMain.tsx';
import { useEffect } from 'react';
import productImg from 'assets/videocard.jpg';
import ProductCharacteristics from './ProductCharacteristics/ProductCharacteristics.tsx';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';

type TProductParams = {
  productId: string;
  productCategory: EComponentTypes;
};

function ProductPage() {
  const { productId, productCategory } = useParams<TProductParams>();
  const navigate = useNavigate();
  useEffect(() => {
    // получение продукта
  }, []);
  return (
    <Container className={styles.container}>
      <ProductMain
        img={productImg}
        name={'Видеокарта'}
        description={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак 8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        mark={4.75}
      />
      <ProductCharacteristics
        characteristics={{
          brand: '1',
          model: '123',
          memoryDb: '4GB',
          memoryType: 'GDDR5',
        }}
      />
    </Container>
  );
}

export default ProductPage;
