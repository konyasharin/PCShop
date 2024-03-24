import styles from './ProductComments.module.css';
import AddComment from './AddComment/AddComment.tsx';
import Comments from './Comments/Comments.tsx';

function ProductComments() {
  return (
    <section className={styles.productComments}>
      <h2>Комментарии</h2>
      <AddComment />
      <Comments />
    </section>
  );
}

export default ProductComments;
