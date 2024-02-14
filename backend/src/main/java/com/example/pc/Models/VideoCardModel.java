package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "VideoCard")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class VideoCardModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name="country")
    private String country;

    @Column(name="memory_db")
    private Integer memory_db;

    @Column(name="memory_type")
    private String memory_type;

    @OneToMany(mappedBy = "videoCardModel", fetch = FetchType.LAZY, cascade=CascadeType.ALL)
    private List<ImageModel> images;

    @Column(name = "preview")
    @Lob
    private byte[] preview;
}
