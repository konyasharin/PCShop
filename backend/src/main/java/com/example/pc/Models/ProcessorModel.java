package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Table(name="Processor")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class ProcessorModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name="description")
    private String description;

    @Column(name="country")
    private String country;

    @Column(name="clock_frequency")
    private Integer clock_frequency;

    @Column(name="turbo_frequency")
    private Integer turbo_frequency;

    @Column(name="heat_dissipation")
    private Integer heat_dissipation;

    @Column(name = "image")
    @Lob
    private byte[] image;

}
