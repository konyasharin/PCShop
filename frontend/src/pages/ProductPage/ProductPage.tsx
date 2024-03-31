import { useNavigate, useParams } from 'react-router-dom';
import Container from 'components/Container/Container.tsx';
import styles from './ProductPage.module.css';
import ProductMain from './ProductMain/ProductMain.tsx';
import { useEffect, useState } from 'react';
import ProductCharacteristics from './ProductCharacteristics/ProductCharacteristics.tsx';
import componentTypes from 'enums/componentTypes.ts';
import productCharacteristics from 'enums/characteristics/productCharacteristics.ts';
import ProductComments from './ProductComments/ProductComments.tsx';
import getComponent from 'api/components/getComponent.ts';
import TVideoCard from 'types/components/TVideoCard.ts';
import TProcessor from 'types/components/TProcessor.ts';
import config from '../../../config.ts';

type TProductParams = {
  productId: string;
  productCategory: keyof typeof componentTypes;
};

export type TProductCharacteristic = {
  characteristicName: string;
  value: string | number;
};

function ProductPage() {
  const { productId, productCategory } = useParams<TProductParams>();
  const [product, setProduct] = useState<
    TVideoCard<string> | TProcessor<string> | null
  >(null);
  const navigate = useNavigate();

  useEffect(() => {
    let key: keyof typeof componentTypes;
    let isProduct = false;
    for (key in componentTypes) {
      if (key == productCategory && productId) {
        isProduct = true;
        getComponent(key, +productId).then(response => {
          setProduct(response.data);
        });
        break;
      }
    }
    if (!isProduct) {
      navigate('/');
    }
  }, []);
  const characteristics: TProductCharacteristic[] = [];
  if (product) {
    let key: keyof typeof product;
    for (key in product) {
      if (key in productCharacteristics)
        characteristics.push({
          characteristicName:
            productCharacteristics[key as keyof typeof productCharacteristics],
          value: product[key],
        });
    }
  }
  return (
    <Container className={styles.container}>
      <ProductMain
        img={product ? `${config.backupUrl}/${product.image}` : ''}
        name={product ? `${product.brand} ${product.model}` : ''}
        description={product ? product.description : ''}
        mark={4.75}
      />
      <ProductCharacteristics characteristics={characteristics} />
      <ProductComments />
    </Container>
  );
}

export default ProductPage;
