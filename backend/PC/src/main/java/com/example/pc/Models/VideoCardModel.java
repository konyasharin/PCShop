package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name = "VideoCard")
@Data
public class VideoCardModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

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
}
