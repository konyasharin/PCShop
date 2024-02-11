package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name="Processor")
@Data
public class ProcessorModel {

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

    @Column(name="clock_frequency")
    private Integer clock_frequency;

    @Column(name="turbo_frequency")
    private Integer turbo_frequency;

    @Column(name="heat_dissipation")
    private Integer heat_dissipation;

}
