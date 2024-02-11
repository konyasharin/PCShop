package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name="RAM")
@Data
public class RAMModel {

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

    @Column(name="frequency")
    private Integer frequency;

    @Column(name="timings")
    private Integer timings;

    @Column(name="capacity_gb")
    private Integer capacity_gb;
}
