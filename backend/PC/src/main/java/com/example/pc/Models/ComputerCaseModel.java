package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name="ComputerCase")
@Data
public class ComputerCaseModel {

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

    @Column(name="material")
    private String material;

    @Column(name="width")
    private Integer width;

    @Column(name="height")
    private Integer height;

    @Column(name="depth")
    private Integer depth;
}
