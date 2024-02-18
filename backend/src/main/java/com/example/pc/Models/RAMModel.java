package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="RAM")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class RAMModel {

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

    @Column(name="frequency")
    private Integer frequency;

    @Column(name="timings")
    private Integer timings;

    @Column(name="capacity_gb")
    private Integer capacity_gb;

    @Column(name = "image")
    @Lob
    private byte[] image;
}
