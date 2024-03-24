import { useNavigate, useParams } from 'react-router-dom';
import Container from 'components/Container/Container.tsx';
import styles from './ProductPage.module.css';
import ProductMain from './ProductMain/ProductMain.tsx';
import { useEffect } from 'react';
import productImg from 'assets/videocard.jpg';
import ProductCharacteristics from './ProductCharacteristics/ProductCharacteristics.tsx';
import EComponentTypes from 'enums/EComponentTypes.ts';
import EProductCharacteristics from 'enums/characteristics/EProductCharacteristics.ts';
import ProductComments from './ProductComments/ProductComments.tsx';

type TProductParams = {
  productId: string;
  productCategory: EComponentTypes;
};

export type TProductCharacteristic = {
  characteristicName: EProductCharacteristics;
  value: string;
};

function ProductPage() {
  const { productId, productCategory } = useParams<TProductParams>();
  const product = {
    brand: '1',
    model: '123',
    memoryDb: '4GB',
    memoryType: 'GDDR5',
  };

  useEffect(() => {
    // получение продукта
  }, []);
  let key: keyof typeof product;
  const characteristics: TProductCharacteristic[] = [];
  for (key in product) {
    characteristics.push({
      characteristicName: EProductCharacteristics[key],
      value: product[key],
    });
  }
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
      <ProductCharacteristics characteristics={characteristics} />
      <ProductComments />
    </Container>
  );
}

export default ProductPage;
